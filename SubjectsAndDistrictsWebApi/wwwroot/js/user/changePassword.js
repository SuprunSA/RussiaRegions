import { apiMethods } from "../requests.js";
import { render } from "../renderHeader.js";

window.addEventListener('load', () => {
    render();

    let submit = document.getElementById('submit');
    let password = document.getElementById('password');
    let acceptPassword = document.getElementById('accept-password');
    password.addEventListener('input', () => {
        if (password.value !== acceptPassword.value) {
            submit.setAttribute('disabled', undefined);
        } else submit.removeAttribute('disabled');
    });

    acceptPassword.addEventListener('input', () => {
        if (password.value !== acceptPassword.value) {
            submit.setAttribute('disabled', undefined);
        } else submit.removeAttribute('disabled');
    })

    let form = document.getElementById('new-password-form');
    form.addEventListener('submit', (evt) => {
        evt.preventDefault();

        apiMethods.post('account/password', {
                pass: acceptPassword.value
            })
            .then(() => {
                console.log('успешно изменён');
            })
            .catch((error) => {
                console.warn(error);
            });
    });

    let showPassword = document.getElementById('show-password');
    showPassword.addEventListener('click', (evt) => {
        evt.preventDefault();

        let password = document.getElementById('password');
        let acceptPassword = document.getElementById('accept-password');

        if (showPassword.innerText === 'Показать пароль') {
            showPassword.innerText = 'Скрыть пароль';
            password.setAttribute('type', 'text');
            acceptPassword.setAttribute('type', 'text');
        } else {
            showPassword.innerText = 'Показать пароль';
            password.setAttribute('type', 'password');
            acceptPassword.setAttribute('type', 'password');
        }
    })
});