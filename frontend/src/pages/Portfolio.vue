<template>
  <layout-baseline
    :headerName="this.headerName"
    :isUser="this.isUser"
  >
    <v-container>
      <!-- <v-layout row wrap justify-end>
        <v-btn color="primary" flat icon :to="{ name: 'profile'}">
          <v-icon dark>edit</v-icon>
        </v-btn>
        <v-btn color="primary" flat icon>
          <v-icon dark>publish</v-icon>
        </v-btn>
      </v-layout> -->
      <v-layout row wrap>
        <v-flex xs12 md4 pa-1>
          <profile
            :account="account"
          />
        </v-flex>
        <v-flex xs12 md7 offset-md1 pa-1>
          <v-timeline
            align-top
            dense
          >
            <timeline-certificate
              v-for="(certificate, index) in certificates"
              :key="index"
              :certificate="certificate"
            />
          </v-timeline>
        </v-flex>
      </v-layout>
    </v-container>
  </layout-baseline>
</template>

<script>
import { mapActions, mapState } from 'vuex'
import TimelineCertificate from '@/components/TimelineCertificate'
import Profile from '@/components/Profile'

export default {
  components: {
    TimelineCertificate,
    Profile
  },
  data: function () {
    return {
      headerName: 'プロフィール',
      isUser: true
    }
  },
  computed: {
    ...mapState('auth', ['account']),
    ...mapState('certificate', ['certificates'])
  },
  created: function () {
    this.initialize()
  },
  methods: {
    ...mapActions({
      fetchProofs: 'certificate/fetchProofs'
    }),
    initialize: function () {
      this.fetchProofs()
    }
  }
}
</script>
