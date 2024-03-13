import React, { useState } from "react";
import { Link, useLocation } from "react-router-dom";
import { IconContext } from "react-icons";
import { BsBank2 } from "react-icons/bs";


export const Navbar = () => {
  const location = useLocation();
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  const handleMobileMenuToggle = () => {
    setMobileMenuOpen(!mobileMenuOpen);
  };

  return (
    <nav className="bg-gray-800">
      <div className="mx-auto max-w-7xl px-2 sm:px-6 lg:px-8">
        <div className="relative flex h-16 items-center justify-between">
          <div className="absolute inset-y-0 left-0 flex items-center sm:hidden">
            {/* Mobile menu button */}
            <button
              type="button"
              className="relative inline-flex items-center justify-center rounded-md p-2 text-gray-400 hover:bg-gray-700 hover:text-white focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white"
              aria-controls="mobile-menu"
              aria-expanded={mobileMenuOpen}
              onClick={handleMobileMenuToggle}
            >
              <span className="absolute -inset-0.5"></span>
              <span className="sr-only">Open main menu</span>
              {/* Icon when menu is closed */}
              <svg
                className={`h-6 w-6 ${mobileMenuOpen ? "hidden" : "block"}`}
                fill="none"
                viewBox="0 0 24 24"
                strokeWidth="1.5"
                stroke="currentColor"
                aria-hidden="true"
              >
                <path strokeLinecap="round" strokeLinejoin="round" d="M3.75 6.75h16.5M3.75 12h16.5m-16.5 5.25h16.5" />
              </svg>
              {/* Icon when menu is open */}
              <svg
                className={`h-6 w-6 ${mobileMenuOpen ? "block" : "hidden"}`}
                fill="none"
                viewBox="0 0 24 24"
                strokeWidth="1.5"
                stroke="currentColor"
                aria-hidden="true"
              >
                <path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12" />
              </svg>
            </button>
          </div>
          <div className="flex flex-1 items-center justify-center sm:items-stretch sm:justify-start">
            <div className="flex flex-shrink-0 items-center">
              <Link to="/">
                <IconContext.Provider value={{ color: "white", size: "2.5rem" }}>
                  <BsBank2 />
                </IconContext.Provider>
              </Link>
            </div>
            <div className="hidden sm:ml-6 sm:block">
              <div className="flex space-x-4">
                <NavLink to="/" label="Home" isActive={location.pathname === "/"} />
                <NavLink to="/clientes" label="Clientes" isActive={location.pathname === "/clientes"} />
                <NavLink to="/contas" label="Contas" isActive={location.pathname === "/contas"} />
                <NavLink to="/cartoes" label="Cartões" isActive={location.pathname === "/cartoes"} />
                <NavLink to="/apolices" label="Apólices" isActive={location.pathname === "/apolices"} />
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Mobile menu */}
      <div className={`sm:hidden ${mobileMenuOpen ? "block" : "hidden"}`} id="mobile-menu">
        <div className="space-y-1 px-2 pb-3 pt-2">
          <NavLink to="/" label="Home" isActive={location.pathname === "/"} />
          <NavLink to="/clientes" label="Clientes" isActive={location.pathname === "/clientes"} />
          <NavLink to="/contas" label="Contas" isActive={location.pathname === "/contas"} />
          <NavLink to="/cartoes" label="Cartões" isActive={location.pathname === "/cartoes"} />
          <NavLink to="/apolices" label="Apólices" isActive={location.pathname === "/apolices"} />
        </div>
      </div>
    </nav>
  );
};

interface NavLinkProps {
  to: string;
  label: string;
  isActive: boolean;
}

const NavLink: React.FC<NavLinkProps> = ({ to, label, isActive }) => {
  return (
    <Link
      to={to}
      className={`${
        isActive ? "bg-gray-900 text-white" : "text-gray-300 hover:bg-gray-700 hover:text-white"
      } block rounded-md px-3 py-2 text-sm font-medium`}
      aria-current={isActive ? "page" : undefined}
    >
      {label}
    </Link>
  );
};
