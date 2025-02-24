// Assuming you have the apiClient function defined elsewhere in your project, as shown in the example you provided:

import { apiClient } from '../utility/apiClientUtility.js';

// Get Ticket by ID
export const getTicketByIdAsync = async (ticketId) => {
    return await apiClient(`/ticket/getticketbyidasync/${ticketId}`, 'GET');
};

// Remove Ticket from User
export const removeTicketFromUserAsync = async (ticketId) => {
    return await apiClient('/ticket/removeticketfromuserasync', 'POST', { ticketId });
};

// Assign Ticket to User
export const assignTicketToUserAsync = async (ticketId, estimationTime) => {
    return await apiClient('/ticket/assigntickettouserasync', 'POST', { ticketId, estimationTime });
};

// Set Estimated Completion Date for Reassign Ticket
export const setEstimatedCompletionDateForReassignTicketAsync = async (ticketId, estimationTime) => {
    return await apiClient('/ticket/setestimatedcompletiondateforreassignticketasync', 'POST', { ticketId, estimationTime });
};

// Update Ticket With Auto Stage
export const updateTicketWithAutoStageAsync = async (ticketId, status, isFinished, message) => {
    return await apiClient('/ticket/updateticketwithautostageasync', 'POST', { ticketId, status, isFinished, message });
};

// Add Ticket
export const addTicketAsync = async (ticketDto) => {
    return await apiClient('/ticket/addticketasync', 'POST', ticketDto);
};

// Get Ticket Status Dropdown
export const getTicketStatusDropdown = async () => {
    return await apiClient('/ticket/getticketstatusdropdown', 'GET');
};

// Get All Free Members Dropdown
export const getAllFreeMembersDropdownAsync = async (projectId) => {
    return await apiClient(`/ticket/getallfreemembersdropdownasync?projectId=${projectId}`, 'GET');
};

// Check Estimated Completion Date
export const checkEstimatedCompletionDateAsync = async (ticketId) => {
    return await apiClient(`/ticket/checkestimatedcompletiondateasync/${ticketId}`, 'GET');
};

// Reassign Ticket to Another User
export const reAssignTicketAsync = async (ticketId, userId) => {
    return await apiClient('/ticket/reassignticketasync', 'POST', { ticketId, userId });
};
