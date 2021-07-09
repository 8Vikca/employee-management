import Vue from 'vue'
import VueRouter from 'vue-router'
import Active from '@/views/Active'
import Inactive from '@/views/Inactive'
import Positions from '@/views/Positions'

Vue.use(VueRouter)

const routes = [
  {
    path: '/active',
    name: 'Active',
    component: Active
  },
  {
    path: '/',
    redirect: '/active',
  },
  {
    path: '/inactive',
    name: 'Inactive',
    component: Inactive
  },
  {
    path: '/positions',
    name: 'Positions',
    component: Positions
  },
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    //component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
