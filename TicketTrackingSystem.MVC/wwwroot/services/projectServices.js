import { apiClient } from '../utility/apiClientUtility.js';

// Project CRUD Operations
export const createProjectAsync = async (createProjectDto) => {
    return await apiClient(
        '/project/createprojectasync',
        'POST',
        createProjectDto
    );
};

export const updateProjectAsync = async (updateProjectDto) => {
    return await apiClient(
        '/project/updateprojectasync',
        'POST',
        updateProjectDto
    );
};

export const deleteProjectAsync = async (projectId) => {
    return await apiClient(
        '/project/deleteprojectasync',
        'POST',
        { projectId: projectId }
    );
};

export const deleteProjectCascadeAsync = async (projectId) => {
    return await apiClient(
        '/project/deleteprojectcascadeasync',
        'POST',
        { projectId: projectId }
    );
};

// Project User Management
export const setUserForProjectAsync = async (userId, projectId, stage) => {
    return await apiClient(
        '/project/setuserforprojectasync',
        'POST',
        {
            userId: userId,
            projectId: projectId,
            stage: stage
        }
    );
};

export const removeUserFromProjectAsync = async (userId, projectId) => {
    return await apiClient(
        '/project/removeuserfromprojectasync',
        'POST',
        {
            userId: userId,
            projectId: projectId
        }
    );
};

// Project Data Endpoints
export const getStageDropdown = async () => {
    return await apiClient(
        '/project/getstagedropdown',
        'GET'
    );
};

export const getAllProjectPaginatedAsync = async (dataTablesRequest) => {
    return await apiClient(
        '/project/getallprojectpaginatedasync',
        'POST',
        { dataTablesRequest: dataTablesRequest }
    );
};

export default {
    createProjectAsync,
    updateProjectAsync,
    deleteProjectAsync,
    deleteProjectCascadeAsync,
    setUserForProjectAsync,
    removeUserFromProjectAsync,
    getStageDropdown,
    getAllProjectPaginatedAsync
};