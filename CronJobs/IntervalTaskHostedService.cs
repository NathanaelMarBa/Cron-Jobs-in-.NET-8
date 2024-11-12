using System.IO;
namespace CronJobs
{
    public class IntervalTaskHostedService : IHostedService, IDisposable
    {
        private Timer _timer; //Ayuda a controlar el tiempo de ejecucion
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = 
                new Timer(SaveFile, null, TimeSpan.Zero, TimeSpan.FromSeconds(10)); //Los parametros pasan el tipo de tarea a realizar y los intevalos de tiempo
            return Task.CompletedTask; //Retorna la tarea ejecutada
        }

        public void SaveFile(object state)
        {
            string nameFile = "a" + new Random().Next(1000) + ".txt";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp", nameFile);
            File.Create(path);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0); //Accion a realizar cuando se detiene el servidor
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose(); //Elimina la basura generada al detener los procesos
        }
    }
}
