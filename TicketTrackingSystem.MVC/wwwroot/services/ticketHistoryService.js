import { apiClient } from '../utility/apiClientUtility.js';

// GET ALL TICKET HISTORY FOR REPORT
export const getAllTicketHistoryForReportAsync = async (request) => {
    return await apiClient(
        '/tickethistory/getalltickethistoryforreportasync',
        'POST',
        {
            dataTablesRequest: request.dataTablesRequest,
            stageFilter: request.stageFilter,
            deliveryStatusFilter: request.deliveryStatusFilter
        }
    );
};

// GET DELIVERY STATUS DROPDOWN
export const getDeliveryStatusDropdown = async () => {
    return await apiClient(
        '/tickethistory/deliverystatusdropdown',
        'GET'
    );
};

export default {
    getAllTicketHistoryForReportAsync,
    getDeliveryStatusDropdown
};