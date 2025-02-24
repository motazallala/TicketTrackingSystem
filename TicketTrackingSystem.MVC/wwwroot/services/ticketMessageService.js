import { apiClient } from '../utility/apiClientUtility.js';

// GET ALL TICKET MESSAGES PAGINATED
export const getAllTicketMessagesPaginatedAsync = async (dataTablesRequest, ticketId) => {
    return await apiClient(
        '/ticketmessage/getallticketmessagespaginatedasync',
        'POST',
        {
            dataTablesRequest: dataTablesRequest,
            ticketId: ticketId
        }
    );
};

// GET ALL NOT SEEN MESSAGES FOR TICKET
export const getAllNotSeenMessageForTicketAsync = async (ticketId) => {
    return await apiClient(
        '/ticketmessage/getallnotseenmessageforticketasync',
        'POST',
        { ticketId: ticketId }
    );
};

// MAKE MESSAGE SEEN
export const makeMessageSeenAsync = async (messageId) => {
    return await apiClient(
        '/ticketmessage/makemessageseenasync',
        'POST',
        { messageId: messageId }
    );
};

export default {
    getAllTicketMessagesPaginatedAsync,
    getAllNotSeenMessageForTicketAsync,
    makeMessageSeenAsync
};