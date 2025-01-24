using Models;

public class Compra {

    public int Id {get;set;}
    public int FkIdUsuario{get; set;}

    public Compra(int id, int fkIdUsuario) {
        Id = id;
        FkIdUsuario = fkIdUsuario;
    }


}