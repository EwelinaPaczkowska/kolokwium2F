using kolokwium2F.DAL;
using kolokwium2F.DTOs;
using kolokwium2F.Exceptions;

namespace kolokwium2F.Services;

public class GalleriesService
{
    private readonly GalleriesDbContext _dbContext;

    public GalleriesService(GalleriesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GalleryDTO> GetPurchasesAsync(int CustomerId, CancellationToken token)
    {
        var customer = await _dbContext.Gallery.FindAsync(CustomerId, token);
        if (customer == null)
            throw new NotFoundException("nie znaleziono klienta");

        var purchasedTicketDto = new GalleryDTO()
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.PhoneNumber
        };
        List<PurchaseDTO> purchases = new List<PurchaseDTO>();

        var pull = await _dbContext.PurchasedTicket.Where(p => p.CustomerId == CustomerId).ToListAsync(token);

        foreach (var pdto in pull)
        {
            var purchaseDto = new PurchaseDTO()
            {
                Date = pdto.Date,
                Price = await _dbContext.TicketConcert.Where(p => p.TicketConcertId == pdto.TicketConcertId)
                    .Select(p => p.Price).FirstOrDefaultAsync(token)
            };
            var ticketDto = await _dbContext.TicketConcert.Where(p => p.TicketConcertId == pdto.TicketConcertId)
                .Select(p => new TicketDTO()
                {
                    SerialNumber = p.Ticket.SerialNumber,
                    SeatNumber = p.Ticket.SeatNumber
                }).FirstOrDefaultAsync(token);

            purchaseDto.ticket = ticketDto;

            var concertDto = await _dbContext.TicketConcert.Where(p => p.TicketConcertId == pdto.TicketConcertId)
                .Select(p => new ConcertDTO()
                {
                    Name = p.Concert.Name,
                    Date = p.Concert.Date
                }).FirstOrDefaultAsync(token);
            purchaseDto.concert = concertDto;
            purchases.Add(purchaseDto);
        }

        purchasedTicketDto.Purchases = purchases;
        return purchasedTicketDto;
    }

    public async Task InsertNewCustomerAsync(CustomerInsertDTO customerDto, CancellationToken token)
{
    await using var transaction = await _dbContext.Database.BeginTransactionAsync(token);

    try
    {
        var customer = await _dbContext.Customer.FindAsync(new object[] { customerDto.Customer.Id }, token);
        if (customer == null)
        {
            customer = new Customer
            {
                CustomerId = customerDto.Customer.Id,
                FirstName = customerDto.Customer.FirstName,
                LastName = customerDto.Customer.LastName,
                PhoneNumber = customerDto.Customer.PhoneNumber
            };
            await _dbContext.Customer.AddAsync(customer, token);
        }

        var groupedByConcert = customerDto.Purchases
            .GroupBy(p => p.ConcertName)
            .ToDictionary(g => g.Key, g => g.Count());

        foreach (var concertName in groupedByConcert.Keys)
        {
            if (groupedByConcert[concertName] > 5)
                throw new BadRequestException($"Nie mozna miec wiecej niz 5 biletow na koncert: {concertName}");
        }

        foreach (var purchase in customerDto.Purchases)
        {
            var concert = await _dbContext.Concert.FirstOrDefaultAsync(c => c.Name == purchase.ConcertName, token);
            if (concert == null)
                throw new NotFoundException($"Koncertu '{purchase.ConcertName}' nie znaleziono");

            var ticket = new Ticket
            {
                TicketId = await _dbContext.Ticket.MaxAsync(t => (int?)t.TicketId, token) ?? 1,
                SerialNumber = $"TK{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                SeatNumber = purchase.seatNumber
            };

            var purchaseRecord = new TicketConcert
            {
                Concert = concert,
                Price = purchase.Price
            };

            await _dbContext.Ticket.AddAsync(ticket, token);
            await _dbContext.TicketConcert.AddAsync(purchaseRecord, token);
        }

        await _dbContext.SaveChangesAsync(token);

        await transaction.CommitAsync(token);
    }
    catch
    {
        await transaction.RollbackAsync(token);
        throw;
    }
}
}