import { apiMethods } from '../requests.js';
import { render } from '../renderHeader.js';

window.addEventListener('load', () => {
    render();

    apiMethods.get('account')
        .then((result) => {
            fillForm(result);
        })
        .catch(error => {
            console.warn(error);
        });
});

function fillForm(user) {
    let form = document.getElementById('edit-user-form');

    form[0].value = user.userName;
    form[1].value = user.email;
    form[2].value = user.firstName;
    form[3].value = user.middleName;
    form[4].value = user.lastName;
    if (user.roles.length === 0) {
        form[5].value = '-';
    } else form[5].value = user.roles.toString();

    form.addEventListener('submit', (evt) => {
        evt.preventDefault();

        apiMethods.put('account', {
                userName: form[0].value,
                email: form[1].value,
                firstName: form[2].value,
                middleName: form[3].value,
                lastName: form[4].value
            })
            .then(() => {
                location.reload();
                console.log('успешно обновлён');
            })
            .catch(error => console.warn(error));
    });
};