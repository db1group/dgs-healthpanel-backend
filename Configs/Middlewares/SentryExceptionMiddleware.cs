using Sentry;

namespace Db1HealthPanelBack.Configs.Middlewares
{
    public class SentryExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public SentryExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);

                throw;
            }
        }
    }
}