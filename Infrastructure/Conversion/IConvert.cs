namespace Infrastructure.Conversion
{
    public interface IConvert<TFrom, TTo>
    {
        TTo Convert(TFrom source);
    }
}