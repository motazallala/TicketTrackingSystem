import { apiClient } from '../utility/apiClientUtility.js';

// Set Role to User
export const setRoleToUserAsync = async (userId, roleId) => {
    return await apiClient('/user/setroletouserasync', 'POST', { userId, roleId });
};

// Create User
export const createUserAsync = async (createUserDto) => {
    return await apiClient('/user/createuserasync', 'POST', createUserDto);
};

// Get User Type Dropdown
export const getUserTypeDropdown = async () => {
    return await apiClient('/user/getusertypedropdown', 'GET');
};

// Remove Role from User
export const removeRoleFromUserAsync = async (userId, roleId) => {
    return await apiClient('/user/removerolefromuserasync', 'POST', { userId, roleId });
};

// Delete User
export const deleteUserAsync = async (userId) => {
    return await apiClient('/user/deleteuserasync', 'POST', { userId });
};

// Delete User Cascade
export const deleteUserCascadeAsync = async (userId) => {
    return await apiClient('/user/deleteusercascadeasync', 'POST', { userId });
};

// Update User
export const updateUserAsync = async (updateUserDto) => {
    return await apiClient('/user/updateuserasync', 'POST', updateUserDto);
};

// Get User by ID
export const getUserByIdAsync = async (userId) => {
    return await apiClient('/user/getuserbyidasync', 'POST', { userId });
};

export default {
    setRoleToUserAsync,
    createUserAsync,
    getUserTypeDropdown,
    removeRoleFromUserAsync,
    deleteUserAsync,
    deleteUserCascadeAsync,
    updateUserAsync,
    getUserByIdAsync
};
