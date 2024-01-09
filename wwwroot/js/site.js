
let deleteBtns = document.querySelectorAll(".del-user");

let addRoleBtn = document.querySelector("#add-role-btn");
let addUserPanel = document.querySelector("#add-user-btn");

let addRolePanel = document.querySelector("#show-box");
let addUserRolePanel = document.querySelector("#new-user-role-panel")


deleteBtns.forEach(x => {
    x.addEventListener('click', () => {
        confirm("Are you sure you want to delete?")
    })
})

if (addRoleBtn != null) {
    addRoleBtn.addEventListener('click', () => {
        addRoleBtn.classList.remove("hide--panel");
        addRolePanel.classList.add("show--panel");
    })
}

if (addUserPanel != null) {
    addUserPanel.addEventListener('click', () => {
        addUserRolePanel.classList.remove("hide");
        addUserRolePanel.classList.add("show--panel");
    })
}