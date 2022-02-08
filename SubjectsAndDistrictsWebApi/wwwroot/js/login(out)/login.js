import { apiMethods } from "../requests.js";
import { render } from '../renderHeader.js';

window.addEventListener('load', () => {
    addListeners.login();
    render();
});

const addListeners = {
    login() {
        let loginForm = document.getElementById('login-form');

        loginForm.addEventListener('submit', (evt) => {
            evt.preventDefault();
            let form = evt.target;
            let path = 'account/login';

            apiMethods.post(path, {
                    userName: form[0].value,
                    password: form[1].value,
                    rememberMe: form[2].checked
                })
                .then(() => {
                    location = 'https://localhost:5001/index.html'
                    console.log('успешно');
                })
                .catch((error) => console.warn(error));
        });

        let passwordControl = document.getElementById('password-control');
        let password = document.getElementById('password');
        passwordControl.addEventListener('click', (evt) => {
            evt.preventDefault();

            if (passwordControl.innerText === 'Скрыть пароль') {
                passwordControl.innerText = 'Показать пароль';
                password.setAttribute('type', 'password');
            } else {
                passwordControl.innerText = 'Скрыть пароль';
                password.setAttribute('type', 'text');
            }
        });
    }
}