namespace Transverse.Models.DAL
{
    public class DeviceRegistration
    {
        public int Id { get; set; }
        public string PushNotificationDeviceId { get; set; }
        public string DeviceId { get; set; }
        public int DeviceType { get; set; }
        public int BadgeNumber { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}