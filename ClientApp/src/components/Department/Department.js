import React, { useState } from "react";
import { useDepartmentContext } from "./Context";

export const Department = () => {
  const {
    department,
    loading,
    toggle,
    CreateDepartment,
    DeleteDepartment,
    UpdateDepartment,
    editDepartment,
    deptID,
    dept,
    setDept,
  } = useDepartmentContext();
  const [showForm, setshowForm] = useState(false);
  const show_Toggle = () => {
    setshowForm(!showForm);
  };
  return (
    <>
      <h2>Department</h2>
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
                  onClick={() => CreateDepartment(dept)}
                >
                  Add Department
                </button>
              ) : (
                <button
                  className="btn btn-success mb-2"
                  onClick={() => UpdateDepartment(deptID, dept)}
                >
                  Update Department
                </button>
              )}
              <div className="form-group col-sm-5">
                <label htmlFor="departmentName">Department Name</label>
                <input
                  type="text"
                  className="form-control"
                  id="departmentName"
                  placeholder="Enter the Department"
                  value={dept}
                  onChange={(e) => setDept(e.target.value)}
                />
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
                <th>DepartmentID</th>
                <th>DepartmentName</th>
                <th></th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              {department.map((department) => (
                <tr key={department.departmentId}>
                  <td>{department.departmentId}</td>
                  <td>{department.departmentName}</td>
                  <td>
                    <button
                      className="btn btn-warning"
                      onClick={() => editDepartment(department.departmentId)}
                    >
                      Update
                    </button>
                  </td>
                  <td>
                    <button
                      className="btn btn-danger"
                      onClick={() => DeleteDepartment(department.departmentId)}
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
