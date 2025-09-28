import { Outlet } from "react-router-dom";
import TopNav from "../components/TopNav";

export default function RootLayout() {
  return (
    <>
      <TopNav />
      <div style={{ padding: 12, paddingTop: 64 }}>
        <Outlet />
      </div>
    </>
  );
}