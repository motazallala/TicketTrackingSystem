// form-utils.js - Simplified Form Handling

export class FormManager {
    constructor(formSelector, config = {}) {
        this.form = document.querySelector(formSelector);
        this.validationConfig = config.validation || {};
        this.init(config);
    }

    init(config) {
        // Set up default configuration
        this.config = {
            validateOnSubmit: true,
            showAlerts: true,
            resetOnSubmit: false,
            successMessage: 'Form submitted successfully!',
            errorMessage: 'Please fix the errors and try again.',
            ...config
        };

        // Set up form submission
        this.form.addEventListener('submit', async (e) => {
            e.preventDefault();
            await this.handleSubmit();
        });
    }

    // Simplified field creation and addition
    addField(fieldConfig) {
        const field = this.createField(fieldConfig);
        this.form.insertBefore(field, this.form.querySelector('#AllError'));
    }
    // Modified createField method in FormManager class
    createField({
        type = 'text',
        name,
        label,
        required = false,
        placeholder = '',
        value = '',
        validation = [],
        hidden = false,
        options = null  // New options parameter
    }) {
        const div = document.createElement('div');
        div.className = `form-group${hidden ? ' d-none' : ''} mb-2`;

        // Generate input or select HTML
        let fieldHTML;
        if (type === 'select') {
            fieldHTML = this.generateSelectOptions(name, value, required, options);
        } else {
            // Standard input field
            fieldHTML = `
        <input type="${type}" 
               name="${name}" 
               id="${name}"
               class="form-control" 
               placeholder="${placeholder}"
               value="${value}"
               ${required ? 'required' : ''}>
        `;
        }

        div.innerHTML = `
    ${hidden ? '' : `
        <label class="form-label" for="${name}">
            ${label} ${required ? '<span class="required">*</span>' : ''}
        </label>
    `}
    ${fieldHTML}
    <div class="error-message text-danger"></div>
    `;

        // Register validation rules
        if (validation.length) {
            this.validationConfig[name] = validation;
        }

        return div;
    }

    // Separated function to handle select options
    generateSelectOptions(name, value, required, options) {
        let optionsHTML = '';

        if (typeof options === 'string') {
            // Use raw HTML string directly
            optionsHTML = options;
        } else if (options && typeof options === 'object') {
            // Convert object to option tags
            optionsHTML = Object.entries(options)
                .map(([val, text]) => {
                    const selected = val === value ? ' selected' : '';
                    return `<option value="${val}"${selected}>${text}</option>`;
                })
                .join('');
        }

        return `
    <select name="${name}" 
            id="${name}"
            class="form-control"
            ${required ? 'required' : ''}>
        ${optionsHTML}
    </select>
    `;
    }

    async handleSubmit() {
        this.clearErrors();

        if (this.config.validateOnSubmit && !this.validate()) {
            this.config.showAlerts && this.showError(this.config.errorMessage);
            return;
        }

        try {
            const formData = this.serialize();
            await this.config.onSubmit(formData);
            this.config.resetOnSubmit && this.form.reset();
            this.config.showAlerts && this.showSuccess(this.config.successMessage);
        } catch (error) {
            const hasValidationErrors = this.handleServerErrors(error.message);
            if (this.config.showAlerts && !hasValidationErrors) {
                this.showError(error.message);
            }
        }
    }

    handleServerErrors(errorData) {
        let hasErrors = false;
        let handleObjectErrors = (errorData) => {
            let hasErrors = false;
            const generalErrors = [];

            Object.entries(errorData).forEach(([fieldName, errorMessages]) => {
                const messages = Array.isArray(errorMessages) ? errorMessages : errorMessages.split(',');

                const input = this.form.querySelector(`[name="${fieldName.toLowerCase()}"]`);
                if (!input) {
                    generalErrors.push(...messages.map(msg => msg.trim()));
                    return;
                }

                const formGroup = input.closest('.form-group');
                if (!formGroup) return;

                const errorDiv = formGroup.querySelector('.error-message');
                input.classList.add('is-invalid');
                if (errorDiv) {
                    errorDiv.innerHTML = messages
                        .map(msg => msg.trim().replace(/\.+$/, ''))
                        .join('<br>');
                    hasErrors = true;
                }
            });

            displayGeneralErrors(generalErrors);
            return hasErrors;
        }
        let handleStringErrors = (errorData) => {
            let hasErrors = false;
            const generalErrors = [];
            const errorEntries = errorData.split(/,\s*(?=\w+\s*:)/);

            errorEntries.forEach(entry => {
                const parts = entry.split(/:\s*/, 2);
                if (parts.length < 2) {
                    generalErrors.push(entry.trim());
                    return;
                }

                const fieldName = parts[0].trim();
                const errorMessage = parts[1].trim().replace(/\.+$/, '');

                const input = this.form.querySelector(`[name="${fieldName.toLowerCase()}"]`);
                if (!input) {
                    generalErrors.push(errorMessage);
                    return;
                }

                const formGroup = input.closest('.form-group');
                if (!formGroup) return;

                const errorDiv = formGroup.querySelector('.error-message');
                input.classList.add('is-invalid');
                if (errorDiv) {
                    const messages = errorMessage.split(',');
                    errorDiv.innerHTML = messages
                        .map(msg => msg.trim().replace(/\.+$/, ''))
                        .join('<br>');
                    hasErrors = true;
                }
            });

            displayGeneralErrors(generalErrors);
            return hasErrors;
        }
        let displayGeneralErrors = (generalErrors) => {
            if (generalErrors.length > 0) {
                const allErrorDiv = this.form.querySelector('#AllError');
                if (allErrorDiv) {
                    allErrorDiv.classList.add('mt-4');
                    allErrorDiv.textContent = generalErrors.join('. ');
                }
            }
        }



        // Check if errorData is an object
        if (typeof errorData === 'object' && errorData !== null) {
            hasErrors = handleObjectErrors(errorData);
        } else if (typeof errorData === 'string') {
            hasErrors = handleStringErrors(errorData);
        }

        return hasErrors;
    }


    // Automatic validation
    validate() {
        let isValid = true;
        const formData = new FormData(this.form);

        for (const [field, rules] of Object.entries(this.validationConfig)) {
            const value = formData.get(field);
            const input = this.form.querySelector(`[name="${field}"]`);
            const errorDiv = input?.closest('.form-group')?.querySelector('.error-message');

            for (const rule of rules) {
                const [ruleName, param] = rule.split(':');
                const result = ValidationRules[ruleName](value, param);

                if (!result.valid) {
                    isValid = false;
                    input?.classList.add('is-invalid');
                    errorDiv && (errorDiv.textContent = result.message);
                    break;
                }
            }
        }

        return isValid;
    }

    // Helper methods
    serialize() {
        return Object.fromEntries(new FormData(this.form));
    }

    clearErrors() {
        this.form.querySelectorAll('.is-invalid').forEach(el => el.classList.remove('is-invalid'));
        this.form.querySelectorAll('.error-message').forEach(el => el.textContent = '');
    }

    showSuccess(message) {
        alert(message); // Replace with your UI implementation
    }

    showError(message) {
        alert(message); // Replace with your UI implementation
    }
}

// Built-in validation rules
const ValidationRules = {
    required: value => ({
        valid: !!value?.trim(),
        message: 'This field is required'
    }),
    email: value => ({
        valid: /^\S+@\S+\.\S+$/.test(value),
        message: 'Invalid email format'
    }),
    min: (value, min) => ({
        valid: value?.length >= parseInt(min),
        message: `Minimum ${min} characters required`
    }),
    max: (value, max) => ({
        valid: value?.length <= parseInt(max),
        message: `Maximum ${max} characters allowed`
    }),
    passwordStrength: (value, param) => {
        const requirements = param?.split(',') || [];
        const errors = [];
        const minLength = parseInt(requirements.find(r => r.startsWith('min:'))?.split(':')[1] || 8);

        if (value.length < minLength) errors.push(`Minimum ${minLength} characters`);
        if (requirements.includes('upper') && !/[A-Z]/.test(value)) errors.push('one uppercase letter');
        if (requirements.includes('lower') && !/[a-z]/.test(value)) errors.push('one lowercase letter');
        if (requirements.includes('number') && !/\d/.test(value)) errors.push('one number');
        if (requirements.includes('special') && !/[!@#$%^&*()]/.test(value)) errors.push('one special character');

        return {
            valid: errors.length === 0,
            message: errors.length ? `Password needs: ${errors.join(', ')}` : ''
        };
    },
    confirm: (value, param) => ({
        valid: value === document.querySelector(`[name="${param}"]`)?.value,
        message: 'Values do not match'
    }),
    date: value => {
        const dateRegex = /^\d{2}\/\d{2}\/\d{4}$/;
        if (typeof value === 'string' && !dateRegex.test(value)) {
            return {
                valid: false,
                message: 'Date must be in DD/MM/YYYY format (e.g., 20/02/2025)'
            };
        }
        const date = new Date(value);
        const isValid = !isNaN(date.getTime());
        return {
            valid: isValid,
            message: 'Invalid date value'
        };
    },
    dateFuture: value => {
        const dateRegex = /^\d{2}\/\d{2}\/\d{4}$/;
        if (typeof value === 'string' && !dateRegex.test(value)) {
            return {
                valid: false,
                message: 'Date must be in DD/MM/YYYY format (e.g., 20/02/2025)'
            };
        }
        const date = new Date(value);
        return {
            valid: date > new Date(),
            message: 'Must be a future date'
        };
    },
    datePast: value => {
        const dateRegex = /^\d{2}\/\d{2}\/\d{4}$/;
        if (typeof value === 'string' && !dateRegex.test(value)) {
            return {
                valid: false,
                message: 'Date must be in DD/MM/YYYY format (e.g., 20/02/2025)'
            };
        }
        const date = new Date(value);
        return {
            valid: date < new Date(),
            message: 'Must be a past date'
        };
    },
    phone: value => ({
        valid: /^(\+\d{1,3}[- ]?)?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$/.test(value),
        message: 'Invalid phone number format'
    }),
    url: value => ({
        valid: /^(https?:\/\/)?([\da-z.-]+)\.([a-z.]{2,6})([/\w .-]*)*\/?$/.test(value),
        message: 'Invalid URL format'
    }),
    numeric: value => ({
        valid: !isNaN(value) && !isNaN(parseFloat(value)),
        message: 'Must be a number'
    }),
    range: (value, param) => {
        const [min, max] = param.split(',').map(Number);
        const num = parseFloat(value);
        return {
            valid: !isNaN(num) && num >= min && num <= max,
            message: `Must be between ${min} and ${max}`
        };
    },
    alphanumeric: value => ({
        valid: /^[a-zA-Z0-9]+$/.test(value),
        message: 'Only letters and numbers allowed'
    }),
    zipCode: value => ({
        valid: /^\d{5}(-\d{4})?$/.test(value),
        message: 'Invalid ZIP code format'
    }),
    regex: (value, param) => {
        try {
            const regex = new RegExp(param);
            return {
                valid: regex.test(value),
                message: 'Does not match required pattern'
            };
        } catch (e) {
            return { valid: false, message: 'Invalid regex pattern' };
        }
    },
    fileType: (value, param) => {
        const allowedTypes = param.split(',');
        const extension = value.split('.').pop().toLowerCase();
        return {
            valid: allowedTypes.includes(extension),
            message: `Allowed types: ${allowedTypes.join(', ')}`
        };
    },
    fileSize: (value, param) => {
        const maxSizeKB = parseInt(param);
        const file = document.querySelector(`[name="${value}"]`)?.files[0];
        return {
            valid: file?.size <= maxSizeKB * 1024,
            message: `File size must be under ${maxSizeKB}KB`
        };
    },
    checkboxRequired: value => ({
        valid: !!value,
        message: 'This checkbox must be checked'
    }),
    ipAddress: value => ({
        valid: /^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(value),
        message: 'Invalid IP address format'
    }),
    creditCard: value => {
        //This is Luhn Algorithm
        const sanitized = value.replace(/[ -]/g, '');
        if (!/^\d{13,16}$/.test(sanitized)) return { valid: false, message: 'Invalid credit card number' };

        let sum = 0;
        let double = false;
        for (let i = sanitized.length - 1; i >= 0; i--) {
            let digit = parseInt(sanitized[i], 10);
            if (double) {
                digit *= 2;
                if (digit > 9) digit -= 9;
            }
            sum += digit;
            double = !double;
        }
        return {
            valid: sum % 10 === 0,
            message: 'Invalid credit card number'
        };
    },
    unique: (value, param) => ({
        valid: !param.split(',').includes(value),
        message: 'Value must be unique'
    })
};

// Usage Example:
/*
const contactForm = new FormManager('#contact-form', {
  onSubmit: async (data) => {
    // Your submission logic here
    console.log('Submitting:', data);
  },
  validation: {
    email: ['required', 'email'],
    password: ['required', 'min:8']
  }
});

contactForm.addField({
  name: 'email',
  label: 'Email Address',
  type: 'email',
  required: true,
  validation: ['required', 'email']
});

contactForm.addField({
  name: 'message',
  label: 'Your Message',
  type: 'textarea',
  validation: ['required', 'min:20']
});
*/