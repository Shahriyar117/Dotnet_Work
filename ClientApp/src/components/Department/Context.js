import React, { useContext, useReducer, useEffect, useState } from "react";
import reducer from "./reducer";

let DEPT_API = "department";
const initialState = {
  department: [],
  loading: true,
  toggle: true,
};

//Create Context
const DepartmentContext = React.createContext();

const DepartmentProvider = ({ children }) => {
  //useState
  const [dept, setDept] = useState("");
  const [stateChange, setStateChange] = useState(false);
  const [deptID, setDeptID] = useState("");
  //useReducer
  const [state, dispatch] = useReducer(reducer, initialState);
  // Get Data
  const fetchApiData = async (url) => {
    dispatch({ type: "GET_LOADING" });
    try {
      const res = await fetch(url);
      const data = await res.json();
      console.log(data);
      dispatch({
        type: "GET_DEPARTMENT",
        payload: data,
      });
    } catch (error) {
      console.log(error);
    }
  };
  //Post Data in Department
  const CreateDepartment = (deptName) => {
    fetch(`${DEPT_API}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        departmentName: deptName,
      }),
    }).then(() => {
      setStateChange(!stateChange);
    });
  };
  //Delete Data in Department
  const DeleteDepartment = (deptID) => {
    fetch(`${DEPT_API}/${deptID}`, {
      method: "DELETE",
    }).then(() => {
      setStateChange(!stateChange);
    });
  };
  //Update Data in Department
  const UpdateDepartment = async (id, dept) => {
    const requestOptions = {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ departmentName: dept, departmentId: id }),
    };
    await fetch(`${DEPT_API}/${id}`, requestOptions).then(() => {
      setStateChange(!stateChange);
    });
    dispatch({
      type: "SET_TOGGLE",
      payload: true,
    });
    setDeptID("");
  };
  //Edit the Department
  const editDepartment = (id) => {
    const dept_edit = state.department.find((currentElement) => {
      return currentElement.departmentId === id;
    });
    setDept(dept_edit.departmentName);
    setDeptID(dept_edit.departmentId);
    dispatch({
      type: "SET_TOGGLE",
      payload: false,
    });
  };

  //UseEffect
  useEffect(() => {
    fetchApiData(`${DEPT_API}`);
  }, [stateChange]);

  return (
    <DepartmentContext.Provider
      value={{
        ...state,
        CreateDepartment,
        DeleteDepartment,
        UpdateDepartment,
        editDepartment,
        deptID,
        dept,
        setDept,
      }}
    >
      {children}
    </DepartmentContext.Provider>
  );
};

//Custom Hook Creation
const useDepartmentContext = () => {
  return useContext(DepartmentContext);
};

export { DepartmentContext, DepartmentProvider, useDepartmentContext };

//Using axios Get Data
/*
    const fetchapidata = () => {
        axios.get('department').then((getdata) => {
            dispatch({ type: "set_department", payload: getdata.data });
            dispatch({ type: "set_loading" })
        }).catch((err) => {
            console.log(err);
        });
        const data = response.json();
    }
    */

//Using Axios Post Method
/*
    const CreateDepartment = () => {
        axios.post("department", {
            dept,
        })
        setDept("");
    }
    */
