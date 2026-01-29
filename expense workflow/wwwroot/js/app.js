document.getElementById("loginForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const response = await fetch("/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password })
    });

    if (!response.ok) {
        alert("Invalid credentials");
        return;
    }

    const data = await response.json();

    localStorage.setItem("token", data.token);
    localStorage.setItem("role", data.role);

    window.location.href = "/dashboard.html";
});


document.addEventListener("DOMContentLoaded", () => {
    console.log("Frontend loaded. Backend integration pending.");
});

function fakeLogin() {
    alert("Login clicked. Backend authentication will be wired later.");
}

function submitExpense() {
    alert("Expense submitted (UI only). Backend workflow coming next.");
}

function approveExpense() {
    alert("Expense approved (UI only).");
}

function rejectExpense() {
    alert("Expense rejected (UI only).");
}

const API_BASE = "https://localhost:5001/api";

function saveToken(token) {
    localStorage.setItem("jwt", token);
}

function getToken() {
    return localStorage.getItem("jwt");
}

async function login() {
    const email = document.querySelector("#email").value;
    const password = document.querySelector("#password").value;

    const res = await fetch(`${API_BASE}/auth/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password })
    });

    if (!res.ok) {
        alert("Login failed");
        return;
    }

    const data = await res.json();
    saveToken(data.token);
    window.location.href = "dashboard.html";
}

async function uploadReceipt(file) {
    const res = await fetch(
        "https://YOUR_PROJECT_ID.supabase.co/storage/v1/object/receipts/" + file.name,
        {
            method: "POST",
            headers: {
                "Authorization": "Bearer YOUR_SUPABASE_ANON_KEY",
                "Content-Type": file.type
            },
            body: file
        }
    );

    if (!res.ok) throw new Error("Upload failed");

    return file.name;
}

function parseJwt(token) {
    const base64 = token.split('.')[1];
    return JSON.parse(atob(base64));
}

function applyRoleVisibility() {
    const token = getToken();
    if (!token) return;

    const claims = parseJwt(token);
    const role = claims["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    if (role !== "Manager") {
        document.querySelector("#approvalsLink")?.remove();
    }
}

async function loadEmployeeDashboard() {
    const token = getToken();

    const res = await fetch(`${API_BASE}/dashboard/employee`, {
        headers: {
            "Authorization": `Bearer ${token}`
        }
    });

    const data = await res.json();

    document.getElementById("submitted").innerText = data.totalSubmitted;
    document.getElementById("approved").innerText = data.totalApproved;
    document.getElementById("rejected").innerText = data.totalRejected;
}

