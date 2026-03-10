const API = "https://localhost:5025/api"; 
// if your backend runs on another port, change this

async function register() {
  const data = {
    username: document.getElementById("regUsername").value,
    password: document.getElementById("regPassword").value,
    role: document.getElementById("regRole").value
  };

  const response = await fetch(`${API}/auth/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data)
  });

  const result = await response.text();
  alert(result);

  if (response.ok) window.location.href = "login.html";
}

async function login() {
  const data = {
    username: document.getElementById("loginUsername").value,
    password: document.getElementById("loginPassword").value
  };

  const response = await fetch(`${API}/auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data)
  });

  const result = await response.json();

  if (!response.ok) {
    alert(result.message || "Login failed");
    return;
  }

  localStorage.setItem("token", result.token);
  localStorage.setItem("role", result.role);
  localStorage.setItem("username", result.username);

  window.location.href = "dashboard.html";
}

function loadDashboard() {
  const role = localStorage.getItem("role");
  const username = localStorage.getItem("username");

  document.getElementById("welcome").innerText = `Welcome, ${username} (${role})`;

  if (role === "Employee") {
    document.getElementById("employeeSection").style.display = "block";
  }
  else if (role === "Manager") {
    document.getElementById("managerSection").style.display = "block";
  }
  else if (role === "Admin") {
    document.getElementById("adminSection").style.display = "block";
  }
}

async function applyLeave() {
  const data = {
    leaveType: document.getElementById("leaveType").value,
    startDate: document.getElementById("startDate").value,
    endDate: document.getElementById("endDate").value,
    reason: document.getElementById("reason").value
  };

  const response = await fetch(`${API}/leave/apply`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": "Bearer " + localStorage.getItem("token")
    },
    body: JSON.stringify(data)
  });

  const result = await response.text();
  alert(result);
}

async function loadLeaves() {
  const role = localStorage.getItem("role");
  const endpoint = role === "Employee" ? "/leave/my-leaves" : "/leave/all";

  const response = await fetch(`${API}${endpoint}`, {
    headers: {
      "Authorization": "Bearer " + localStorage.getItem("token")
    }
  });

  const data = await response.json();
  const tableBody = document.getElementById("leaveTableBody");
  tableBody.innerHTML = "";

  data.forEach(leave => {
    const row = document.createElement("tr");

    row.innerHTML = `
      <td>${leave.id}</td>
      <td>${leave.employeeName || localStorage.getItem("username")}</td>
      <td>${leave.leaveType}</td>
      <td>${leave.startDate.split("T")[0]}</td>
      <td>${leave.endDate.split("T")[0]}</td>
      <td>${leave.reason}</td>
      <td>${leave.status}</td>
      <td>
        ${
          role === "Manager" || role === "Admin"
            ? `
              <button onclick="approveLeave(${leave.id})">Approve</button>
              <button onclick="rejectLeave(${leave.id})">Reject</button>
            `
            : "-"
        }
      </td>
    `;

    tableBody.appendChild(row);
  });
}

async function approveLeave(id) {
  const response = await fetch(`${API}/leave/approve/${id}`, {
    method: "PUT",
    headers: {
      "Authorization": "Bearer " + localStorage.getItem("token")
    }
  });

  const result = await response.text();
  alert(result);
  loadLeaves();
}

async function rejectLeave(id) {
  const response = await fetch(`${API}/leave/reject/${id}`, {
    method: "PUT",
    headers: {
      "Authorization": "Bearer " + localStorage.getItem("token")
    }
  });

  const result = await response.text();
  alert(result);
  loadLeaves();
}

function logout() {
  localStorage.clear();
  window.location.href = "login.html";
}