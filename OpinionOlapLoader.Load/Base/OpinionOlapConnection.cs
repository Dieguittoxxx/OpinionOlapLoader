using Microsoft.Extensions.Configuration;
using OpinionOlapLoader.Common.Data;

namespace OpinionOlapLoader.Load.Base;

public abstract class OpinionOlapConnection(IConfiguration configuration)
    : SqlServerConnection(configuration, "OlapConnection")
{
}
