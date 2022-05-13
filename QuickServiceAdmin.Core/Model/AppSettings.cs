using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace QuickServiceAdmin.Core.Model
{
    [ExcludeFromCodeCoverage]
    public class AppSettings
    {
        public static string EnableSwagger { get; set; }
        public static string LogPath { get; set; }
        public static string ValidateToken { get; set; }
        public static List<string> FileTypes { get; set; }
        public static string LookUpFolder { get; set; }
        public static string PostingFileExcelFolder { get; set; }
        public static string FirstAppDeploymentDate { get; set; }
        public static bool ConnectToLocalRedis { get; set; }
        public static string SetRedisApi { get; set; }
        public static string GetRedisApi { get; set; }
    }
}