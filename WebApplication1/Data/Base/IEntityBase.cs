namespace WebApplication1.Data.Base
{
    public interface IEntityBase
    {
        int Id { get; set; } //bütün tablolarda Id değişkeni olduğu için hepsi bunu implement etmek zorunda.
    }
}
