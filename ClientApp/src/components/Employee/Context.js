import React, { useContext, useReducer, useEffect, useState } from "react";
import reducer from "./reducer";

let EMP_API = "employee";
const initialState = {
  employee: [],
  loading: true,
};

//Create Context
const EmployeeContext = React.createContext();

const EmployeeProvider = ({ children }) => {
  //useState
  const [toggle, setToggle] = useState(true);
  const [stateChange, setStateChange] = useState(false);
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
        type: "GET_EMPLOYEE",
        payload: data,
      });
    } catch (error) {
      console.log(error);
    }
  };
  //Post Data in Employee
  const CreateEmployee = (emp) => {
    console.log(emp);
    fetch(`${EMP_API}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(emp),
    }).then(() => {
      setStateChange(!stateChange);
    });
  };
  //Delete Data in Employee
  const DeleteEmployee = (empID) => {
    fetch(`${EMP_API}/${empID}`, {
      method: "DELETE",
    }).then(() => {
      setStateChange(!stateChange);
    });
  };
  //Update Data in Employee
  const UpdateEmployee = async (emp) => {
    const requestOptions = {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(emp),
    };
    await fetch(`${EMP_API}/${emp.employeeId}`, requestOptions).then(() => {
      setStateChange(!stateChange);
    });
    setToggle(true);
  };
  //UseEffect
  useEffect(() => {
    fetchApiData(`${EMP_API}`);
  }, [stateChange]);

  return (
    <EmployeeContext.Provider
      value={{
        ...state,
        CreateEmployee,
        DeleteEmployee,
        UpdateEmployee,
        toggle,
        setToggle,
      }}
    >
      {children}
    </EmployeeContext.Provider>
  );
};

//Custom Hook Creation
const useEmployeeContext = () => {
  return useContext(EmployeeContext);
};

export { EmployeeContext, EmployeeProvider, useEmployeeContext };
