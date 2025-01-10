const API_URL = "/api/v1/switch";
const HUB_URL = "/switch-hub";

const connection = new signalR.HubConnectionBuilder()
    .withUrl(HUB_URL)
    .withAutomaticReconnect()
    .configureLogging(signalR.LogLevel.Information) 
    .build();

connection.on("ToggleCreated", (toggle) => {
    appendToggleToTable(toggle);
});

connection.on("ToggleUpdated", (toggle) => {
    updateToggleInTable(toggle);
});

connection.on("ToggleDeleted", (id) => {
    removeToggleFromTable(id);
});

connection.start()
    .then(() => console.log("SignalR connected successfully"))
    .catch((err) => console.error("SignalR connection failed:", err));

function appendToggleToTable(toggle) {
    const tableBody = document.querySelector("#toggles-table tbody");
    const row = document.createElement("tr");
    row.id = `toggle-${toggle.id}`;

    row.innerHTML = `
        <td>${toggle.name}</td>
        <td>${toggle.description}</td>
        <td>${toggle.isEnabled ? "Enabled" : "Disabled"}</td>
        <td>
            <button onclick="toggleStatus('${toggle.id}')">${toggle.isEnabled ? "Disable" : "Enable"}</button>
            <button onclick="deleteToggle('${toggle.id}')">Delete</button>
        </td>
    `;

    tableBody.appendChild(row);
}

function updateToggleInTable(toggle) {
    const row = document.querySelector(`#toggle-${toggle.id}`);
    if (row) {
        row.innerHTML = `
            <td>${toggle.name}</td>
            <td>${toggle.description}</td>
            <td>${toggle.isEnabled ? "Enabled" : "Disabled"}</td>
            <td>
                <button onclick="toggleStatus('${toggle.id}')">${toggle.isEnabled ? "Disable" : "Enable"}</button>
                <button onclick="deleteToggle('${toggle.id}')">Delete</button>
            </td>
        `;
    }
}

function removeToggleFromTable(id) {
    const row = document.querySelector(`#toggle-${id}`);
    if (row) row.remove();
}

async function loadToggles() {
    const response = await fetch(API_URL);
    const toggles = await response.json();

    const tableBody = document.querySelector("#toggles-table tbody");
    tableBody.innerHTML = "";

    toggles.forEach((toggle) => appendToggleToTable(toggle));
}

async function toggleStatus(id) {
    const toggle = await fetch(`${API_URL}/${id}`).then((res) => res.json());
    toggle.isEnabled = !toggle.isEnabled;

    await fetch(`${API_URL}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(toggle),
    });

    loadToggles();
}

async function deleteToggle(id) {
    await fetch(`${API_URL}/${id}`, { method: "DELETE" });
    loadToggles();
}

document.querySelector("#create-toggle").addEventListener("click", async () => {
    const name = prompt("Enter toggle name:");
    const description = prompt("Enter toggle description:");

    if (!name || !description) return;

    await fetch(API_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, description, isEnabled: false }),
    });

    loadToggles();
});

const themeToggleButton = document.querySelector("#theme-toggle");
const currentTheme = localStorage.getItem("theme") || "light";
document.body.setAttribute("data-theme", currentTheme);
themeToggleButton.textContent = currentTheme === "dark" ? "☀️ Light Mode" : "🌙 Dark Mode";

themeToggleButton.addEventListener("click", () => {
    const newTheme = document.body.getAttribute("data-theme") === "light" ? "dark" : "light";
    document.body.setAttribute("data-theme", newTheme);
    localStorage.setItem("theme", newTheme);
    themeToggleButton.textContent = newTheme === "dark" ? "☀️ Light Mode" : "🌙 Dark Mode";
});

loadToggles();
