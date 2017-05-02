export class RemarkTypeFormatValueConverter {
    toView(reservationRequestRemarkType) {
        switch (reservationRequestRemarkType) {
            case "ReservationRequestInvoiceComment":
                return 'ReservationRequestInvoiceComment';
                break;
            case "ReservationRequestVolComment":
                return 'ReservationRequestVolComment';
                break;
            case "ReservationRequestSupplierRemark":
                return 'ReservationRequestSupplierRemark';
                break;
            case "ReservationInvoiceComment":
                return 'ReservationInvoiceComment';
                break;
            case "ReservationComment":
                return 'ReservationComment';
                break;
            case "ReservationSupplierRemark":
                return 'ReservationSupplierRemark';
                break;
            case "System":
                return 'System';
                break;
            default:
                return 'Type was not found';
        }
    }
}