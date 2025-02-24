import { apiClient } from '../utility/apiClientUtility.js';

// ADD ROLE TO PERMISSION
export const addRoleToPermissionAsync = async (createPermissionDto) => {
    return await apiClient('/permission/addroletopermissionasync', 'POST', createPermissionDto);
};

// GET ALL PERMISSIONS AS HTML
export const getAllPermissionsAsHtmlAsync = async () => {
    return await apiClient('/permission/getallpermissionsashtmlasync', 'GET');
};

// REMOVE ROLE FROM PERMISSION
export const removeRoleFromPermissionAsync = async (createPermissionDto) => {
    return await apiClient('/permission/removerolefrompermissionasync', 'DELETE', createPermissionDto);
};

// GET ALL ROLES WITH PERMISSIONS PAGINATED
export const getAllRolesWithPermissionPaginatedAsync = async (dataTablesRequest) => {
    return await apiClient('/permission/getallroleswithpermissionpaginatedasync', 'POST', dataTablesRequest);
};

export default {
    addRoleToPermissionAsync,
    getAllPermissionsAsHtmlAsync,
    removeRoleFromPermissionAsync,
    getAllRolesWithPermissionPaginatedAsync
};
