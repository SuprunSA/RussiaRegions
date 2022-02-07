import { apiMethods } from "./requests.js";

export function render() {
    apiMethods.get('account')
        .then((result) => {
            let loginButton = document.getElementById('login-button');
            loginButton.remove();

            let dropdownTemplate = document.getElementById('dropdown');
            let navbar = document.getElementById('navbar');

            navbar.append(dropdownTemplate.content.cloneNode(true));
            let dropdown = document.getElementById('dropdownUserMenu');

            dropdown.setAttribute('value', result.userName);

            let userIcon = document.createElement('i');
            userIcon.classList.add('bi');
            userIcon.classList.add('bi-person-check');
            let userName = result.firstName + ' ' + result.middleName[0] + '.' + result.lastName[0] + '.';
            dropdown.innerText = userName;
            dropdown.prepend(userIcon);

            let logoutButton = document.getElementById('logout');

            logoutButton.addEventListener('click', (evt) => {
                evt.preventDefault();
                apiMethods.post('account/logout')
                    .then(() => render())
                    .catch(error => console.warn(error));
            });

            if (contains(result.roles, 'Admin')) {
                let list = document.getElementsByTagName('ul');
                let item = document.createElement('li');
                let ref = document.createElement('a');
                ref.classList.add('dropdown-item');
                ref.setAttribute('href', '../html/adminOpp.html');
                ref.innerText = 'Управление пользователями';
                item.append(ref);
                list[0].prepend(item);
            }
        })
        .catch(() => {
            let dropdown = document.getElementById('dropdown-menu');
            let loginButton = document.getElementById('login-button');
            if (dropdown) {
                dropdown.remove();
            }
            if (loginButton) {
                loginButton.remove();
            }

            let userButton = document.getElementById('user-button');
            let ref = document.createElement('a');
            ref.classList.add('btn');
            ref.classList.add('btn-secondary');
            ref.classList.add('btn-lg');
            ref.setAttribute('id', 'login-button');
            ref.setAttribute('href', '../html/login.html');
            ref.innerText = 'Вход';

            userButton.append(ref);
        });
};

export function contains(array, elem) {
    return array.indexOf(elem) != -1;
}