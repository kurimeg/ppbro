<template>
  <v-layout class="home-background" align-center justify-center fill-height>
    <v-flex xs12>
      <v-layout row wrap justify-center>
        <v-flex xs12 md3 mx-3>
          <v-text-field
            color="white"
            v-model="profile.name"
            label="氏名"
            required
            outline
          ></v-text-field>
        </v-flex>
        <v-flex xs12 md3 mx-3>
          <v-text-field
            color="white"
            v-model="profile.mail"
            label="メールアドレス"
            required
            outline
          ></v-text-field>
        </v-flex>
      </v-layout>
      <v-layout row wrap justify-center>
        <v-flex xs12 md3 mx-3>
          <v-btn
            color="white"
            outline
            block
            large
            @click="signup(profile)"
          >
            <strong>はじめる</strong>
          </v-btn>
        </v-flex>
      </v-layout>
    </v-flex>
  </v-layout>
</template>

<script>
import { mapActions, mapState } from 'vuex'


export default {
  data: () => ({
    profile: {
      name: '',
      mail: ''
    }
  }),
  methods: {
    ...mapActions({
      signup: 'auth/signup'
    })
  }
}
</script>

<style scoped>
.home-background{
  background: linear-gradient(#F4FFC7, #408C98, #0F2530);
}
</style>
