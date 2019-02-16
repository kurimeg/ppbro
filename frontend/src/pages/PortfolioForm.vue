<template>
  <v-dialog v-model="visible" fullscreen hide-overlay transition="dialog-bottom-transition">
      <v-btn
        fab
        dark
        small
        color="accent"
        slot="activator"
      >
        <v-icon>edit</v-icon>
      </v-btn>
      <v-card>
        <v-toolbar dark color="primary">
          <v-btn icon dark @click="visible = false">
            <v-icon>close</v-icon>
          </v-btn>
          <v-toolbar-title>Edit</v-toolbar-title>
          <!-- <v-spacer></v-spacer>
          <v-toolbar-items>
            <v-btn dark flat @click="visible = false">Save</v-btn>
          </v-toolbar-items> -->
        </v-toolbar>

        <v-container>
          <v-layout row wrap>
            <v-flex xs12 md4 pa-1>
              <profile
                :account="account"
              />
            </v-flex>
            <v-flex xs12 md7 offset-md1 pa-1>
              <v-container fluid grid-list-sm>
                <v-layout row wrap>
                  <timeline-certificate
                    v-for="(certificate, index) in certificates"
                    :key="index"
                    :certificate="certificate"
                  />
                  <v-flex xs12 md6 pa-2>
                    <div class="certificate-add">
                      <issuer-selecter />
                    </div>
                  </v-flex>
                </v-layout>
              </v-container>
            </v-flex>
          </v-layout>
        </v-container>

      </v-card>
    </v-dialog>
</template>

<script>
import { mapActions, mapState } from 'vuex'
import TimelineCertificate from '@/components/TimelineCertificateEditor'
import Profile from '@/components/ProfileEditor'
import IssuerSelecter from '@/components/IssuerSelecter'

export default {
  components: {
    TimelineCertificate,
    Profile,
    IssuerSelecter
  },
  data: function () {
    return {
      visible: false
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
    },
    onSave: function () {
      this.fetchProofs()
    }
  }
}
</script>

<style scoped>
.certificate-add{
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 20vh;
  border: dashed 1px #489DAB;
}
</style>
