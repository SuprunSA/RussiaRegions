import { render } from '../renderHeader.js';
import { apiMethods } from '../requests.js';

window.addEventListener('load', () => {
    render();

    let form = document.getElementById('new-user-form');
    form.addEventListener('submit', (evt) => {
        evt.preventDefault();

        let roles = [];
        let role = form[5].value;
        if (role !== 'none') {
            roles = [role];
        }

        apiMethods.post('user', {
                userName: form[0].value,
                email: form[1].value,
                firstName: form[2].value,
                middleName: form[3].value,
                lastName: form[4].value,
                roles: roles,
                password: form[6].value
            })
            .then(() => {
                location.pathname = 'html/adminOpp.html';
                console.log('успешно добавлен');
            })
            .catch(error => console.warn(error));
    });
});