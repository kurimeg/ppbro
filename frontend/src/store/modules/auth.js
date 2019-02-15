import router from '@/router'

const state = {
  account: JSON.parse(localStorage.getItem('account'))
}

const mutations = {
  storeAccount (state, certificates) {
    state.certificates = certificates
  }
}

const actions = {
  signup ({ commit }, param) {
    debugger
    const account = { name: param.name, mail: param.mail }
    commit('storeAccount', account)
    localStorage.setItem('account', JSON.stringify(account))
    router.push('portfolio')
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
