import { apiClient } from '../utility/apiClientUtility.js';

// GET ALL ROLES PAGINATED
export const getAllRolesPaginatedAsync = async (dataTablesRequest) => {
    return await apiClient('/role/getallrolespaginatedasync', 'POST', dataTablesRequest);
};

// CREATE ROLE
// If your backend expects an object with a roleName property, you can wrap the roleName accordingly.
export const createRoleAsync = async (roleName) => {
    return await apiClient('/role/createroleasync', 'POST',  roleName );
};

// DELETE ROLE
export const deleteRoleAsync = async (roleName) => {
    return await apiClient(`/role/deleteroleasync/${roleName}`, 'DELETE');
};

export const deleteRoleCascadeAsync = async (roleName) => {
    return await apiClient(`/role/deleterolecascadeasync/${roleName}`, 'DELETE');
};
// UPDATE ROLE
export const updateRoleAsync = async (updateRoleDto) => {
    return await apiClient('/role/updateroleasync', 'PUT', updateRoleDto);
};

// GET ALL ROLES AS HTML
export const getAllRolesAsHtmlAsync = async () => {
    return await apiClient('/role/getallrolesashtmlasync', 'GET');
};

export default {
    getAllRolesPaginatedAsync,
    createRoleAsync,
    deleteRoleAsync,
    updateRoleAsync,
    getAllRolesAsHtmlAsync
};