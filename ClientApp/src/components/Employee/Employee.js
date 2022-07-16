import React, { useState } from "react";
//import { useEmployeeContext } from "./Context";
import { useGlobalContext } from "../GlobalContext/GlobalContext";
import { useDepartmentContext } from "../Department/Context";
import { useEmployeeContext } from "./Context";
export const Employee = () => {
  const {
    employee,
    loading,
    toggle,
    CreateEmployee,
    DeleteEmployee,
    UpdateEmployee,
    setToggle,
  } = useEmployeeContext();
  const { department } = useDepartmentContext();
  const [showForm, setshowForm] = useState(false);
  let initial = {
    employeeName: "",
    email: "",
    address: "",
    phone: "",
    departmentId: "",
  };
  //useState
  const [emp, setEmp] = useState(initial);
  //Show_Toggle
  const show_Toggle = () => {
    setshowForm(!showForm);
  };
  // handle onChange event of the dropdown

  //Edit the Employee
  const editEmployee = (id) => {
    const emp_edit = employee.find((currentElement) => {
      return currentElement.employeeId === id;
    });
    setEmp({
      ...emp_edit,
      employeeName: emp_edit.employeeName,
      email: emp_edit.email,
      address: emp_edit.address,
      phone: emp_edit.phone,
      departmentId: emp_edit.departmentId,
    });
    setToggle(false);
  };
  //getDepartmentByID
  const getDepartmentById = (id) => {
    const dep = department.find((dept) => {
      return dept.departmentId === id;
    });
    return dep?.departmentName;
  };

  return (
    <>
      <h2>Employee</h2>
      <div className="container">
        {showForm ? (
          <button className="float-end btn btn-dark" onClick={show_Toggle}>
            Hide Form
          </button>
        ) : (
          <button className="float-end btn btn-dark" onClick={show_Toggle}>
            Show Form
          </button>
        )}
        <div className="mx-auto">
          {showForm ? (
            <form className=" m-2 p-2">
              {toggle ? (
                <button
                  className="btn btn-success mb-2"
                  onClick={() => CreateEmployee(emp)}
                >
                  Add Employee
                </button>
              ) : (
                <button
                  className="btn btn-success mb-2"
                  onClick={() => UpdateEmployee(emp)}
                >
                  Update Employee
                </button>
              )}
              <div className="form-group col-sm-5">
                <label htmlFor="employeeName">Employee Name</label>
                <input
                  type="text"
                  className="form-control"
                  id="employeeName"
                  placeholder="Enter the Employee"
                  value={emp.employeeName}
                  onChange={(e) => {
                    setEmp({ ...emp, employeeName: e.target.value });
                  }}
                />
                <label htmlFor="email">Email</label>
                <input
                  type="text"
                  className="form-control"
                  id="email"
                  placeholder="Enter the Email"
                  value={emp.email}
                  onChange={(e) => {
                    setEmp({ ...emp, email: e.target.value });
                  }}
                />
                <label htmlFor="address">Address</label>
                <input
                  type="text"
                  className="form-control"
                  id="address"
                  placeholder="Enter the Address"
                  value={emp.address}
                  onChange={(e) => {
                    setEmp({ ...emp, address: e.target.value });
                  }}
                />
                <label htmlFor="phone">Phone</label>
                <input
                  type="text"
                  className="form-control"
                  id="phone"
                  placeholder="Enter the Phone"
                  value={emp.phone}
                  onChange={(e) => {
                    setEmp({ ...emp, phone: e.target.value });
                  }}
                />
                <label htmlFor="departmentId">Department ID</label>
                <select
                  className="form-select mt-2"
                  id="departmentId"
                  value={emp.departmentId}
                  onChange={(e) => {
                    setEmp({ ...emp, departmentId: e.target.value });
                  }}
                >
                  <option value="Select Department">Select Department</option>
                  {department &&
                    department.map((dep) => {
                      return (
                        <option key={dep.departmentId} value={dep.departmentId}>
                          {dep.departmentName}
                        </option>
                      );
                    })}
                </select>
              </div>
            </form>
          ) : null}
        </div>
        {loading ? (
          <p>
            <em>Loading...</em>
          </p>
        ) : (
          <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
              <tr>
                <th>EmployeeID</th>
                <th>EmployeeName</th>
                <th>Email</th>
                <th>Address</th>
                <th>Phone</th>
                <th>Department</th>
                <th></th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              {employee.map((employee) => (
                <tr key={employee.employeeId}>
                  <td>{employee.employeeId}</td>
                  <td>{employee.employeeName}</td>
                  <td>{employee.email}</td>
                  <td>{employee.address}</td>
                  <td>{employee.phone}</td>
                  <td>{getDepartmentById(employee.departmentId)}</td>
                  <td>
                    <button
                      className="btn btn-warning"
                      onClick={() => editEmployee(employee.employeeId)}
                    >
                      Update
                    </button>
                  </td>
                  <td>
                    <button
                      className="btn btn-danger"
                      onClick={() => DeleteEmployee(employee.employeeId)}
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </>
  );
};
