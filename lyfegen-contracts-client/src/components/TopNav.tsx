import { NavLink } from "react-router-dom";

const linkStyle: React.CSSProperties = { marginRight: 16, textDecoration: "none" };
const activeStyle: React.CSSProperties = { textDecoration: "underline" };

export default function TopNav() {
  return (
    <nav
      style={{
        position: "fixed",
        top: 0,
        left: 0,
        right: 0,
        height: 48,
        display: "flex",
        alignItems: "center",
        padding: "0 16px",
        borderBottom: "1px solid #ddd",
        background: "#fff",
        zIndex: 10
      }}
    >
      <NavLink to="/patients" style={linkStyle} end>
        {({ isActive }) => <span style={isActive ? activeStyle : undefined}>Patients</span>}
      </NavLink>
      <NavLink to="/contracts" style={linkStyle} end>
        {({ isActive }) => <span style={isActive ? activeStyle : undefined}>Contracts</span>}
      </NavLink>
    </nav>
  );
}