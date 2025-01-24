using Models;

public class LineaPedido {

  public int Id {get; set;}
  public int FkIdCompra {get; set;}
  public int FkIdProducto{get; set;}

  public LineaPedido(int id, int fkIdCompra, int fkIdProducto) {
    Id = id;
    FkIdCompra = fkIdCompra;
    FkIdProducto = fkIdProducto; 
  }

}