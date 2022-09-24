// See https://aka.ms/new-console-template for more information
public enum Extracciones

{
    CajeroHumano = 0,
    CajeroAutomatico = 1,
}
public class cuentaBancaria {
    protected double saldo;

    public virtual double extraer(double monto, Extracciones tipoExtraccion)
    {
        saldo -= monto;
        return monto;
    }

    public void insertar(double monto)
    {
        saldo +=monto;
    }
}

public class CorrientePesos : cuentaBancaria
{
    public double extraer(double monto, Extracciones tipoExtraccion){
        if ( this.saldo - monto <= -5000)
        {
            Console.WriteLine("No cuenta con suficientes fodos. El saldo menor que puede tener en la cuenta es -5000");
            return 0;
        }else if (tipoExtraccion == Extracciones.CajeroAutomatico && monto > 20000)
        {
            Console.WriteLine("Recuerde que solo puede retirar $20.000 por cajero automatico");
            saldo -= 20000;
            return 20.000;
        } else
        {
            saldo -= monto;
            return monto;
        }

    }
}

public class CorrienteDolares : cuentaBancaria
{
    private DateTime fechaUltimaExtraccion;
    private double extraccionDeHoy;
    CorrienteDolares()
    {
        extraccionDeHoy=0;
    }

    public double extraer(double monto, Extracciones tipoExtraccion)
    {
        if (tipoExtraccion == Extracciones.CajeroAutomatico)
        {
            if (DateTime.Today != fechaUltimaExtraccion.Date)
            {
                extraccionDeHoy = 0;
            }
            if (extraccionDeHoy + monto > 200)
            {
                System.Console.WriteLine("No puede extraer más de 200 dolares por día");
                saldo -= 200 - extraccionDeHoy;
                fechaUltimaExtraccion = DateTime.Today;
                return 200 - extraccionDeHoy;
            } else
            {
                saldo -= monto;
                extraccionDeHoy += monto;
                return monto;
            }
        } else
        {
            saldo -= monto;
            return monto;
        }
    }
}

public class CajaAhorro : cuentaBancaria
{
    public double extraer(double monto, Extracciones tipoExtraccion)
    {
        if (saldo <= 0)
        {
            Console.WriteLine("No tiene fondos en esta cuenta");
            return 0;
        }
        if (tipoExtraccion == Extracciones.CajeroAutomatico && monto > 10000)
        {
            System.Console.WriteLine("Solo puede retirar 10000 pesos por cajero automatico");
            saldo -= 10000;
            return 10000;
        }
        saldo -= monto;
        return monto;
    }
}