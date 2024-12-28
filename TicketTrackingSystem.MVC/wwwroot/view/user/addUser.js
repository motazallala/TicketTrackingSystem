import { userTable } from './userTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { addRoleToPermissionAsync, getUserTypeDropdown } from '../../services/userServices.js';
import { getAllDepartmentsAsHtmlAsync } from '../../services/departmentServices.js';
$(document).ready(function () {
    // Cache for dropdown options to avoid repeated server calls
    let cachedUserTypes = null;
    let cachedDepartments = null;
    $("#addBtn").on("click", async function () {
        //add department modal that has a name and description field and a submit button
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');

        const title = 'Add Department';
        const bodyContent = `
            <div class="form-group">
                <label for="name">First Name :</label>
                <input type="text" class="form-control" id="firstName" name="firstName">
            </div>
            <div class="form-group">
                <label for="lastName">Last Name :</label>
                <input type="text" class="form-control" id="lastName" name="lastName">
            </div>
            <div class="form-group">
                <label for="userName">User Name :</label>
                <input type="text" class="form-control" id="userName" name="userName">
            </div>
            <div class="form-group">
                <label for="email">Email :</label>
                <input type="text" class="form-control" id="email" name="email">
            </div>
            <div class="form-group">
                <label for="phoneNumber">Phone Number :</label>
                <input type="text" class="form-control" id="phoneNumber" name="phoneNumber">
            </div>
            <div class="form-group">
                <label for="password">Password :</label>
                <input type="text" class="form-control" id="password" name="password">
            </div>
            <div class="form-group">
                <label for="userType">User Type :</label>
                <select class="form-control" id="userType" name="userType"></select>
            </div>
            <div class="form-group" id="departmentDropDown">
                <label for="departmentId">Department :</label>
                <select class="form-control" id="departmentId" name="departmentId">
                    <option value="">Select Department</option>
                </select>
            </div>
        `;
        const closeButton = '<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>';
        const submitButton = '<button type="button" class="btn btn-primary" id="submit">Submit</button>';

        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [closeButton, submitButton]);

        try {
            await populateDropdowns();
            toggleDepartmentDropdown();
            $('#myModal').modal('show');

            $('#submit').off('click').on('click', async function () {
                const addUserDto = gatherFormData();
                if (addUserDto.userType === '0') {
                    addUserDto.departmentId = '';
                }
                const addResult = await addRoleToPermissionAsync(JSON.stringify(addUserDto));
                if (addResult.isSuccess) {
                    $('.modal').modal('hide');
                    userTable.ajax.reload(null, false);
                }
                else {
                    showErrorModal(addResult.error.description);
                }

            });
            $('#userType').off('change').on('change', toggleDepartmentDropdown);
        }
        catch (e) {
            showErrorModal('An error occurred while loading the data.');
        }
    });

    function gatherFormData() {
        return {
            firstName: $('#firstName').val(),
            lastName: $('#lastName').val(),
            userName: $('#userName').val(),
            email: $('#email').val(),
            phoneNumber: $('#phoneNumber').val(),
            password: $('#password').val(),
            userType: $('#userType').val(),
            departmentId: $('#departmentId').val()
        };
    }

    async function populateDropdowns() {
        if (!cachedUserTypes) {
            cachedUserTypes = await getUserTypeDropdown();
        }
        if (cachedUserTypes.isSuccess) {
            $('#userType').html(cachedUserTypes.data);
        }
        else {
            showErrorModal(cachedUserTypes.error.description);
        }
        if (!cachedDepartments) {
            cachedDepartments = await getAllDepartmentsAsHtmlAsync();
        }
        if (cachedDepartments.isSuccess) {
            $('#departmentId').append(cachedDepartments.value);
        }
        else {
            showErrorModal(cachedDepartments.error.description);
        }
    }
    function toggleDepartmentDropdown() {
        const userType = $('#userType').val();
        if (userType === '0') {
            $('#departmentDropDown').hide();
        } else {
            $('#departmentDropDown').show();
        }
    }
});