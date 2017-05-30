using System;
namespace Reflect.Integration.API
{
    public abstract class ReportRunner
    {
        public abstract Report Execute(ReportSettings settings, Statement statement);
    }
}
