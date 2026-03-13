const API_BASE = "http://localhost:5196/api"; // change port if needed

function getToken() {
  return localStorage.getItem("token");
}

function getRole() {
  return localStorage.getItem("role");
}

function logout() {
  localStorage.clear();
  window.location.href = "login.html";
}

async function registerUser() {
  const body = {
    name: document.getElementById("regName").value,
    email: document.getElementById("regEmail").value,
    phone: document.getElementById("regPhone").value,
    password: document.getElementById("regPassword").value
  };

  const res = await fetch(`${API_BASE}/Auth/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body)
  });

  const data = await res.json();
  document.getElementById("message").innerText = data.message || "Done";
}

async function loginUser() {
  const body = {
    email: document.getElementById("loginEmail").value,
    password: document.getElementById("loginPassword").value
  };

  const res = await fetch(`${API_BASE}/Auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body)
  });

  const data = await res.json();

  if (!res.ok) {
    document.getElementById("message").innerText = data.message || "Login failed";
    return;
  }

  localStorage.setItem("token", data.token);
  localStorage.setItem("role", data.role);
  localStorage.setItem("studentId", data.studentId);
  localStorage.setItem("name", data.name);

  if (data.role === "Admin") {
    window.location.href = "admin.html";
  } else {
    window.location.href = "student.html";
  }
}

async function loadDepartments() {
  const res = await fetch(`${API_BASE}/Departments`);
  const departments = await res.json();

  const select = document.getElementById("departmentId");
  if (!select) return;

  select.innerHTML = "";
  departments.forEach(d => {
    select.innerHTML += `<option value="${d.departmentId}">${d.departmentName}</option>`;
  });
}

async function loadCourses() {
  const res = await fetch(`${API_BASE}/Courses`);
  const courses = await res.json();

  const list = document.getElementById("coursesList");
  if (!list) return;

  list.innerHTML = "";

  courses.forEach(course => {
    let buttons = "";

    if (getRole() === "Admin") {
      buttons = `
        <button class="small-btn" onclick="deleteCourse(${course.courseId})">Delete</button>
      `;
    }

    if (getRole() === "Student") {
      buttons = `
        <button class="small-btn" onclick="enrollCourse(${course.courseId})">Enroll</button>
        <button class="small-btn" onclick="dropCourse(${course.courseId})">Drop</button>
      `;
    }

    list.innerHTML += `
      <div class="card">
        <h4>${course.courseName}</h4>
        <p>Department: ${course.department}</p>
        <p>Credits: ${course.credits}</p>
        <p>Seats Available: ${course.seatsAvailable}</p>
        ${buttons}
      </div>
    `;
  });
}

async function searchCourses() {
  const keyword = document.getElementById("searchKeyword").value;
  const res = await fetch(`${API_BASE}/Courses/search?keyword=${encodeURIComponent(keyword)}`);
  const courses = await res.json();

  const list = document.getElementById("coursesList");
  list.innerHTML = "";

  courses.forEach(course => {
    let buttons = "";

    if (getRole() === "Student") {
      buttons = `
        <button class="small-btn" onclick="enrollCourse(${course.courseId})">Enroll</button>
        <button class="small-btn" onclick="dropCourse(${course.courseId})">Drop</button>
      `;
    }

    if (getRole() === "Admin") {
      buttons = `
        <button class="small-btn" onclick="deleteCourse(${course.courseId})">Delete</button>
      `;
    }

    list.innerHTML += `
      <div class="card">
        <h4>${course.courseName}</h4>
        <p>Department: ${course.department}</p>
        <p>Credits: ${course.credits}</p>
        <p>Seats Available: ${course.seatsAvailable}</p>
        ${buttons}
      </div>
    `;
  });
}

async function addCourse() {
  const body = {
    courseName: document.getElementById("courseName").value,
    departmentId: parseInt(document.getElementById("departmentId").value),
    credits: parseInt(document.getElementById("credits").value),
    seatsAvailable: parseInt(document.getElementById("seatsAvailable").value)
  };

  const res = await fetch(`${API_BASE}/Courses`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${getToken()}`
    },
    body: JSON.stringify(body)
  });

  const data = await res.json();
  alert(data.message || "Course added");
  loadCourses();
}

async function deleteCourse(id) {
  const res = await fetch(`${API_BASE}/Courses/${id}`, {
    method: "DELETE",
    headers: {
      "Authorization": `Bearer ${getToken()}`
    }
  });

  const data = await res.json();
  alert(data.message || "Deleted");
  loadCourses();
}

async function enrollCourse(courseId) {
  const res = await fetch(`${API_BASE}/Enrollment/enroll`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${getToken()}`
    },
    body: JSON.stringify({ courseId })
  });

  const data = await res.json();
  alert(data.message || "Enrolled");
  loadCourses();
  loadMyEnrollments();
}

async function dropCourse(courseId) {
  const res = await fetch(`${API_BASE}/Enrollment/drop/${courseId}`, {
    method: "POST",
    headers: {
      "Authorization": `Bearer ${getToken()}`
    }
  });

  const data = await res.json();
  alert(data.message || "Dropped");
  loadCourses();
  loadMyEnrollments();
}

async function loadMyEnrollments() {
  const box = document.getElementById("myEnrollments");
  if (!box) return;

  const res = await fetch(`${API_BASE}/Enrollment/my`, {
    headers: {
      "Authorization": `Bearer ${getToken()}`
    }
  });

  const data = await res.json();
  box.innerHTML = "";

  data.forEach(item => {
    box.innerHTML += `
      <div class="card">
        <h4>${item.courseName}</h4>
        <p>Department: ${item.department}</p>
        <p>Enrollment Date: ${new Date(item.enrollmentDate).toLocaleString()}</p>
        <p>Drop Date: ${item.dropDate ? new Date(item.dropDate).toLocaleString() : "Active"}</p>
      </div>
    `;
  });
}

async function loadEnrollmentHistory() {
  const box = document.getElementById("historyList");
  if (!box) return;

  const res = await fetch(`${API_BASE}/Enrollment/history`, {
    headers: {
      "Authorization": `Bearer ${getToken()}`
    }
  });

  const data = await res.json();
  box.innerHTML = "";

  data.forEach(item => {
    box.innerHTML += `
      <div class="card">
        <h4>${item.courseName}</h4>
        <p>Department: ${item.department}</p>
        <p>Student: ${item.studentName} (${item.studentEmail})</p>
        <p>Enrollment Date: ${new Date(item.enrollmentDate).toLocaleString()}</p>
        <p>Drop Date: ${item.dropDate ? new Date(item.dropDate).toLocaleString() : "Active"}</p>
      </div>
    `;
  });
}