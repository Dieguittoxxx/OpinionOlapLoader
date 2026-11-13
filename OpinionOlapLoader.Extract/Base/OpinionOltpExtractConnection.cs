using Microsoft.Extensions.Configuration;
using OpinionOlapLoader.Common.Data;

namespace OpinionOlapLoader.Extract.Base;

public abstract class OpinionOltpExtractConnection(IConfiguration configuration)
    : SqlServerConnection(configuration, "OltpConnection")
{
}
