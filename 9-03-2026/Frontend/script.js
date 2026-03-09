const API = "http://localhost:5161/api/todo";

let tasks = [];
let currentFilter = "all";

async function loadTasks(){

const res = await fetch(API);

tasks = await res.json();

renderTasks();

}

function renderTasks(){

const list = document.getElementById("taskList");

list.innerHTML="";

let filtered = tasks;

if(currentFilter==="active")
filtered = tasks.filter(t=>!t.isCompleted);

if(currentFilter==="completed")
filtered = tasks.filter(t=>t.isCompleted);

filtered.forEach(t=>{

const li=document.createElement("li");

li.innerHTML=`
<input type="checkbox" ${t.isCompleted?"checked":""}
onclick="toggleTask(${t.id})">

<span onclick="editTask(${t.id})"
style="text-decoration:${t.isCompleted?'line-through':'none'}">
${t.title}
</span>

<span class="priority-${t.priority.toLowerCase()}">
${t.priority}
</span>

<button onclick="deleteTask(${t.id})">Delete</button>
`;

list.appendChild(li);

});

}

async function addTask(){

const title=document.getElementById("taskInput").value;

const priority=document.getElementById("priority").value;

await fetch(API,{
method:"POST",
headers:{'Content-Type':'application/json'},
body:JSON.stringify({title,priority})
});

loadTasks();

}

async function deleteTask(id){

await fetch(API + "/" + id,{
method:"DELETE"
});

loadTasks();
}

function filterTasks(type){

currentFilter=type;

renderTasks();

}

function searchTasks(){

const text=document.getElementById("searchBox").value.toLowerCase();

const filtered=tasks.filter(t=>t.title.toLowerCase().includes(text));

renderSearch(filtered);

}

function renderSearch(filtered){

const list=document.getElementById("taskList");

list.innerHTML="";

filtered.forEach(t=>{

const li=document.createElement("li");

li.innerHTML=t.title;

list.appendChild(li);

});

}

async function editTask(id){

const newTitle=prompt("Edit task");

if(!newTitle) return;

await fetch(API+"/edit/"+id,{
method:"PUT",
headers:{'Content-Type':'application/json'},
body:JSON.stringify({title:newTitle})
});

loadTasks();

}

function toggleDarkMode(){

document.body.classList.toggle("dark");

}

async function toggleTask(id){

await fetch(API + "/" + id,{
method:"PUT"
});

loadTasks();
}

loadTasks();