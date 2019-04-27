using System.Linq;

namespace LdJam44.Driver
{
    public class SensorScanResult
    {
        public int LaneNumber { get; set; }
        public bool HasHit { get; set; }
    }
    
    public class SensorsScanResult
    {
        public SensorScanResult[] Results { get; set; }
        public bool Any => Results?.Any(p => p.HasHit) ?? false;
    }
}
