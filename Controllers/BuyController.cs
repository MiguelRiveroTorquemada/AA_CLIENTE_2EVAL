using Microsoft.AspNetCore.Mvc;
using BuyItem;
using Product.Data;

namespace BuyController;

[ApiController]
[Route("[Controller]")]

public class BuyController : ControllerBase
{

    private readonly DataContext _context;

    public BuyController(DataContext dataContext)
    {
        _context = dataContext;
    }

    [HttpGet]
    public ActionResult<List<BuyItems>> Get(){
    return Ok (_context.BuyItem);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<BuyItems> Get(int id)
    {
    var BuyItems = _context.BuyItem.Find(id);
        return BuyItems == null? NotFound()
            : Ok(BuyItems);
    }
    /*
    [HttpGet]
    public ActionResult<List<CarritoItems>> Get()
    {
        List<CarritoItems> carrito = _context.CarritoItem.ToList();
        return carrito == null? NoContent()
            : Ok(carrito);
    }
*/

    [HttpPost]
    public ActionResult<BuyItems> Post([FromBody] BuyItems cesta)
    {
         BuyItems existinBuyItems= _context.BuyItem.Find(cesta.id);
        if (existinBuyItems != null)
        {
            return Conflict("Ya existe un elemento");
        }
        _context.BuyItem.Add(cesta);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + cesta.id;
        return Created(resourceUrl, cesta);
    }

    [HttpPut("{id:int}")]
    public ActionResult<BuyItems> Update([FromBody] BuyItems cesta, int id)
    {
        BuyItems buyItemToUpdate = _context.BuyItem.Find(id);
        if (buyItemToUpdate == null)
        {
            return NotFound("Elemento del carrito no encontrado");
        }
        buyItemToUpdate.amount = cesta.amount;
        buyItemToUpdate.id = cesta.id;
        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + cesta.id;

        return Created(resourceUrl, cesta);
    }
        [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        BuyItems buyItemToDelete = _context.BuyItem.Find(id);
        if (buyItemToDelete == null)
        {
            return NotFound("Elemento de la cesta no se ha encontrado");
        }
        _context.BuyItem.Remove(buyItemToDelete);
        _context.SaveChanges();
        return Ok(buyItemToDelete);
    }
}