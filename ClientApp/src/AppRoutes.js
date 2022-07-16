import { Counter } from "./components/Counter";
import { Department } from "./components/Department/Department";
import { Employee } from "./components/Employee/Employee";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/departments',
    element: <Department />
    },
    {
        path: '/employees',
        element: <Employee />
    },
   
];

export default AppRoutes;
