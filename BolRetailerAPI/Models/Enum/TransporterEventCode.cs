namespace BolRetailerApi.Models.Enum;

public enum TransporterEventCode
{
    UNKNOWN = 0,
    PRE_ANNOUNCED = 1,
    AT_TRANSPORTER = 2,
    IN_TRANSIT = 3,
    DELIVERED_AT_NEIGHBOURS = 4,
    DELIVERED_AT_CUSTOMER = 5,
    PICKED_UP_AT_PICK_UP_POINT = 6,
    AT_PICK_UP_POINT = 7,
    RETURNED_TO_SENDER = 8,
    INBOUND_COLLECT = 9
}