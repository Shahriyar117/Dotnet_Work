import { useDepartmentContext } from "../Department/Context";
import { useEmployeeContext } from "../Employee/Context";
const useGlobalContext = () => {
  return useDepartmentContext(), useEmployeeContext();
};
export { useGlobalContext };
