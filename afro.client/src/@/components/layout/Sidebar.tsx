import { navigationitems } from "@/lib/navigationItems";
import { NavLink } from "react-router-dom";

export function Sidebar(){


  return (
    <aside className="w-64 border-r bg-white">
      <div className="p-6 font-semibold text-xl">
          Site Admin
      </div>
      <nav className="px-3">
        {
          navigationitems.map(item => (
            <NavLink
              key={item.path}
              to={item.path}
              className="flex items-center gap-3 p-3 rounded-xl"
            >
              <item.icon size={18}/>
              {item.label}
            </NavLink>
          ))
        }
      </nav>
    </aside>
  )
}