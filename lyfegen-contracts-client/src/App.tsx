import { createBrowserRouter, RouterProvider } from "react-router-dom";
import RootLayout from "./pages/RootLayout";
import PatientsPage from "./pages/PatientsPage";
import ContractsPage from "./pages/ContractsPage";
import PatientDetailsPage from "./pages/PatientDetailsPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    children: [
      { index: true, element: <PatientsPage /> },
      { path: "patients", element: <PatientsPage /> },
      { path: "patients/:id", element: <PatientDetailsPage /> },
      { path: "contracts", element: <ContractsPage /> }
    ]
  }
]);

export default function App() {
  return <RouterProvider router={router} />;
}
