import { apiClient } from '../utility/apiClientUtility.js';
export const getAllDepartmentsPaginatedAsync = async (dataTablesRequest) => {
    // Only the relative endpoint is needed.
    return await apiClient('/department/getalldepartmentspaginatedasync', 'POST', dataTablesRequest);
};



export const createDepartmentAsync = async (createDepartmentDto) => {
    return await apiClient('/department/createdepartmentasync', 'POST', createDepartmentDto);
};


export const deleteDepartmentAsync = async (departmentId) => {
    // Pass the departmentId as part of the endpoint.
    return await apiClient(`/department/deletedepartmentasync/${departmentId}`, 'DELETE');
};
export const deleteDepartmentCascadeAsync = async (departmentId) => {
    // Pass the departmentId as part of the endpoint.
    return await apiClient(`/department/deletedepartmentcascadeasync/${departmentId}`, 'DELETE');
};

export const updateDepartmentAsync = async (updateDepartmentDto) => {
    return await apiClient('/department/updatedepartmentasync', 'PUT', updateDepartmentDto);
};

export const getAllDepartmentsAsHtmlAsync = async () => {
    return await apiClient('/department/getalldepartmentsashtmlasync', 'GET');
};