// Assuming the apiClient function is defined elsewhere in your project, similar to the previous example.
import { apiClient } from '../utility/apiClientUtility.js';

// Get Total Member Tickets
export const getTotalMemberTicketAsync = async () => {
    return await apiClient('/home/gettotalmemberticketasync', 'GET');
};

// Get Total Client Tickets
export const getTotalClientTicketAsync = async () => {
    return await apiClient('/home/gettotalclientticketasync', 'GET');
};

// Get Total Users
export const getTotalUsersAsync = async () => {
    return await apiClient('/home/gettotalusersasync', 'GET');
};

// Get Total Tickets
export const getTotalTicketsAsync = async () => {
    return await apiClient('/home/gettotalticketsasync', 'GET');
};

// Exporting all dashboard methods in case you want to import them all together
export default {
    getTotalMemberTicketAsync,
    getTotalClientTicketAsync,
    getTotalUsersAsync,
    getTotalTicketsAsync,
};

