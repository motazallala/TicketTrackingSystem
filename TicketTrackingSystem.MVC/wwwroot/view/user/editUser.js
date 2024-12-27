import { userTable } from './userTable.js';
import { setupModalData, showErrorModal } from '../../utility/dataModalUtility.js';
import { updateUserAsync, getUserByIdasync, getUserTypeDropdown } from '../../services/userServices.js';
import { getAllDepartmentsAsHtmlAsync } from '../../services/departmentServices.js';

$(document).ready(function () {
    // Cache for dropdown options to avoid repeated server calls
    let cachedUserTypes = null;
    let cachedDepartments = null;

    userTable.on('click', '.dt-edit', async function () {
        const data = userTable.row($(this).parents('tr')).data();
        const modalTitle = $('.modal .modal-title');
        const modalBody = $('#modelBody');
        const modalFooter = $('.modal .modal-footer');
        
        // Modal content
        const title = 'Edit User';
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
        const viewButton = `<button type="button" class="btn btn-default" onclick="$('#myModal').modal('hide')" data-dismiss="modal">Close</button>`;
        const editDepartment = '<button type="button" class="btn btn-primary" id="submit">Submit</button>';

        setupModalData(modalTitle, modalBody, modalFooter, title, bodyContent, [viewButton, editDepartment]);

        try {
            // Populate User Data
            const result = await getUserByIdasync(data.id);
            if (result.isSuccess) {
                populateUserForm(result.data);
            } else {
                showErrorModal(result.error.description);
                return;
            }

            // Populate dropdowns (user types and departments)
            await populateDropdowns(result.data.userTypeNumber, result.data.departmentId);

            // Toggle Department Dropdown visibility based on user type
            toggleDepartmentDropdown();

            // Show modal
            $('#myModal').modal('show');

            // Submit button click event handler
            $('#submit').off('click').on('click', async function () {
                const editUserDto = gatherFormData(data.id);
                if (editUserDto.userType === '0') {
                    editUserDto.departmentId = '';
                }
                const updateResult = await updateUserAsync(JSON.stringify(editUserDto));

                if (updateResult.isSuccess) {
                    $('#myModal').modal('hide');
                    userTable.ajax.reload(null, false);
                } else {
                    showErrorModal(updateResult.error.description);
                }
            });

            // On user type change, toggle Department visibility
            $('#userType').off('change').on('change', toggleDepartmentDropdown);
            
        } catch (error) {
            showErrorModal('An error occurred while loading the data.');
        }
    });

    // Populates user form fields with the data
    function populateUserForm(userData) {
        $('#firstName').val(userData.firstName);
        $('#lastName').val(userData.lastName);
        $('#userName').val(userData.userName);
        $('#email').val(userData.email);
        $('#phoneNumber').val(userData.phoneNumber);
        $('#userType').val(userData.userTypeNumber);
        $('#departmentId').val(userData.departmentId);
    }

    // Populates user type and department dropdowns
    async function populateDropdowns(selectedUserType, selectedDepartment) {
        // Fetch user types if not cached
        if (!cachedUserTypes) {
            cachedUserTypes = await getUserTypeDropdown();
        }
        if (cachedUserTypes.isSuccess) {
            $('#userType').html(cachedUserTypes.data).val(selectedUserType);
        }

        // Fetch departments if not cached
        if (!cachedDepartments) {
            cachedDepartments = await getAllDepartmentsAsHtmlAsync();
        }
        if (cachedDepartments.isSuccess) {
            $('#departmentId').append(cachedDepartments.value).val(selectedDepartment);
        }
    }

    // Gather form data into a DTO
    function gatherFormData(userId) {
        return {
            id: userId,
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

    // Toggle department dropdown visibility based on user type
    function toggleDepartmentDropdown() {
        const userType = $('#userType').val();
        if (userType === '0') {
            $('#departmentDropDown').hide();
        } else {
            $('#departmentDropDown').show();
        }
    }
});
