import router from '@/router'

const state = {
  account: JSON.parse(localStorage.getItem('account'))
}

const mutations = {
  storeAccount (state, account) {
    state.account = account
  }
}

const actions = {
  signup ({ commit }, param) {
    const account = {
      name: param.name,
      nameEng: '',
      photoSrc: '',
      mail: param.mail,
      birthday: '',
      detail: ''
    }
    commit('storeAccount', account)
    localStorage.setItem('account', JSON.stringify(account))
    router.push('portfolio')
  },
  edit ({ commit }, param) {
    commit('storeAccount', param)
    localStorage.setItem('account', JSON.stringify(param))
    return new Promise((resolve, reject) => {
      resolve()
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
