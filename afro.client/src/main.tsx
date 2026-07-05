import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './styles/global.css'
import { RouterProvider } from 'react-router-dom'
import { router } from './app/routes/rotuer.ts'
import { store } from './app/store.ts'
import { Provider } from 'react-redux'
import { AuthInitializer } from './features/auth/components/AuthInitializer.tsx'
import { NotificationProvider } from './@/components/providers/NotificationProvider.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider store={store}>
      <NotificationProvider />
      <AuthInitializer>
    <RouterProvider router={router} />
      </AuthInitializer>
    </Provider>
  </StrictMode>,
)
